
/* Name: main.c
 * Project: AVR USB driver for CDC interface on Low-Speed USB
 *              for ATtiny2313
 * Author: Osamu Tamura
 * Creation Date: 2007-10-03
 * Tabsize: 4
 * Copyright: (c) 2007-2009 by Recursion Co., Ltd.
 * License: Proprietary, free under certain conditions. See Documentation.
 *
 */

#include <string.h>
#include <avr/io.h>
#include <avr/interrupt.h>
#include <avr/pgmspace.h>
#include <avr/wdt.h>

#include "usbdrv.h"
#include "oddebug.h"


enum {
    SEND_ENCAPSULATED_COMMAND = 0,
    GET_ENCAPSULATED_RESPONSE,
    SET_COMM_FEATURE,
    GET_COMM_FEATURE,
    CLEAR_COMM_FEATURE,
    SET_LINE_CODING = 0x20,
    GET_LINE_CODING,
    SET_CONTROL_LINE_STATE,
    SEND_BREAK
};


static PROGMEM char configDescrCDC[] = {   /* USB configuration descriptor */
    9,          /* sizeof(usbDescrConfig): length of descriptor in bytes */
    USBDESCR_CONFIG,    /* descriptor type */
    67,
    0,          /* total length of data returned (including inlined descriptors) */
    2,          /* number of interfaces in this configuration */
    1,          /* index of this configuration */
    0,          /* configuration name string index */
#if USB_CFG_IS_SELF_POWERED
    USBATTR_SELFPOWER,  /* attributes */
#else
    USBATTR_BUSPOWER,   /* attributes */
#endif
    USB_CFG_MAX_BUS_POWER/2,            /* max USB current in 2mA units */

    /* interface descriptor follows inline: */
    9,          /* sizeof(usbDescrInterface): length of descriptor in bytes */
    USBDESCR_INTERFACE, /* descriptor type */
    0,          /* index of this interface */
    0,          /* alternate setting for this interface */
    USB_CFG_HAVE_INTRIN_ENDPOINT,   /* endpoints excl 0: number of endpoint descriptors to follow */
    USB_CFG_INTERFACE_CLASS,
    USB_CFG_INTERFACE_SUBCLASS,
    USB_CFG_INTERFACE_PROTOCOL,
    0,          /* string index for interface */

    /* CDC Class-Specific descriptor */
    5,           /* sizeof(usbDescrCDC_HeaderFn): length of descriptor in bytes */
    0x24,        /* descriptor type */
    0,           /* header functional descriptor */
    0x10, 0x01,

    4,           /* sizeof(usbDescrCDC_AcmFn): length of descriptor in bytes */
    0x24,        /* descriptor type */
    2,           /* abstract control management functional descriptor */
    0x02,        /* SET_LINE_CODING,    GET_LINE_CODING, SET_CONTROL_LINE_STATE    */

    5,           /* sizeof(usbDescrCDC_UnionFn): length of descriptor in bytes */
    0x24,        /* descriptor type */
    6,           /* union functional descriptor */
    0,           /* CDC_COMM_INTF_ID */
    1,           /* CDC_DATA_INTF_ID */

    5,           /* sizeof(usbDescrCDC_CallMgtFn): length of descriptor in bytes */
    0x24,        /* descriptor type */
    1,           /* call management functional descriptor */
    3,           /* allow management on data interface, handles call management by itself */
    1,           /* CDC_DATA_INTF_ID */

    /* Endpoint Descriptor */
    7,           /* sizeof(usbDescrEndpoint) */
    USBDESCR_ENDPOINT,  /* descriptor type = endpoint */
    0x80|USB_CFG_EP3_NUMBER,        /* IN endpoint number */
    0x03,        /* attrib: Interrupt endpoint */
    8, 0,        /* maximum packet size */
    USB_CFG_INTR_POLL_INTERVAL,        /* in ms */

    /* Interface Descriptor  */
    9,           /* sizeof(usbDescrInterface): length of descriptor in bytes */
    USBDESCR_INTERFACE,           /* descriptor type */
    1,           /* index of this interface */
    0,           /* alternate setting for this interface */
    2,           /* endpoints excl 0: number of endpoint descriptors to follow */
    0x0A,        /* Data Interface Class Codes */
    0,
    0,           /* Data Interface Class Protocol Codes */
    0,           /* string index for interface */

    /* Endpoint Descriptor */
    7,           /* sizeof(usbDescrEndpoint) */
    USBDESCR_ENDPOINT,  /* descriptor type = endpoint */
    0x01,        /* OUT endpoint number 1 */
    0x02,        /* attrib: Bulk endpoint */
    8, 0,        /* maximum packet size */
    0,           /* in ms */

    /* Endpoint Descriptor */
    7,           /* sizeof(usbDescrEndpoint) */
    USBDESCR_ENDPOINT,  /* descriptor type = endpoint */
    0x81,        /* IN endpoint number 1 */
    0x02,        /* attrib: Bulk endpoint */
    8, 0,        /* maximum packet size */
    0,           /* in ms */
};


uchar usbFunctionDescriptor(usbRequest_t *rq)
{

    if(rq->wValue.bytes[1] == USBDESCR_DEVICE){
        usbMsgPtr = (uchar *)usbDescriptorDevice;
        return usbDescriptorDevice[0];
    }else{  /* must be config descriptor */
        usbMsgPtr = (uchar *)configDescrCDC;
        return sizeof(configDescrCDC);
    }
}

/* ------------------------------------------------------------------------- */
/* ----------------------------- USB interface ----------------------------- */
/* ------------------------------------------------------------------------- */

uchar usbFunctionSetup(uchar data[8])
{
usbRequest_t    *rq = (void *)data;

    if((rq->bmRequestType & USBRQ_TYPE_MASK) == USBRQ_TYPE_CLASS){    /* class request type */

        if( rq->bRequest==GET_LINE_CODING ){
            return 0xff;
            /*    GET_LINE_CODING -> usbFunctionRead()    */
        }
    }

    return 0;
}

/*---------------------------------------------------------------------------*/
/* usbFunctionRead                                                           */
/*---------------------------------------------------------------------------*/

uchar usbFunctionRead( uchar *data, uchar len )
{

    /*  reply USART configuration	*/
#define	BAUD	9600
	data[0]	= BAUD & 0xff;
	data[1]	= (uchar)(BAUD>>8);
//	0,					//	1 stop bit
//	0,					//	None parity
//	8					//	8 data bits
	memset( data+2, 0, 4 );
	data[6]	= 8;

    return 7;
}


#define	TX_SIZE		4
#define	TX_MASK		(TX_SIZE-1)
#define	RX_SIZE		4

/* command buffer */
static uchar    rx_buf[RX_SIZE], tx_buf[TX_SIZE];
static uchar    iwptr, uwptr;
static uchar    tos, nos;


static uchar u2h( uchar u )
{
	if( u>9 )
		u	+= 7;
	return u+'0';
}

static uchar h2u( uchar h )
{
	h	-= '0';
	if( h>9 )
		h	-= 7;
    return h;
}

void usbFunctionWriteOut( uchar *data, uchar len )
{

    /*  postpone receiving next data    */
    usbDisableAllRequests();

    /*    host => device : Request  */
    do {
        char    c;

        //    delimiter?
        c    = *data++;
        if( c>0x20 ) {
            if( c & 0x40 )
                c   &= 0x5f;        //    to upper case
            tx_buf[uwptr++]    = c;
            uwptr   &= TX_MASK;
            continue;
        }

        //    command
        if( uwptr==1 ) {
            volatile uchar  *addr = (uchar *)((unsigned int)tos);
            uchar   x;

           	//    get
			if( tx_buf[0]=='?' ) {
                x   = *addr;
                rx_buf[0]   = u2h(x>>4);
                rx_buf[1]   = u2h(x&0x0f);
				iwptr	= 4;
			}
            //    set
			else if( tx_buf[0]=='=' ) {
                cli();
                *addr   = nos;
                sei();
				iwptr	= 2;
			}
            //    xor & set
			else if( tx_buf[0]=='^' ) {
                cli();
                *addr   ^= nos;
                sei();
				iwptr	= 2;
			}
            rx_buf[2]   = '\r';
            rx_buf[3]   = '\n';
        }

        //    number
        else {
            nos = tos;
            tos = (h2u(tx_buf[0])<<4) | h2u(tx_buf[1]);
        }
        uwptr   = 0;

    } while(--len);

    usbEnableAllRequests();
}


static void hardwareInit(void)
{
uchar    i, j;

    /* activate pull-ups except on USB lines */
    USB_CFG_IOPORT   = (uchar)~((1<<USB_CFG_DMINUS_BIT)|(1<<USB_CFG_DPLUS_BIT));
    /* all pins input except USB (-> USB reset) */
#ifdef USB_CFG_PULLUP_IOPORT    /* use usbDeviceConnect()/usbDeviceDisconnect() if available */
    USBDDR    = 0;    /* we do RESET by deactivating pullup */
    usbDeviceDisconnect();
#else
    USBDDR    = (1<<USB_CFG_DMINUS_BIT)|(1<<USB_CFG_DPLUS_BIT);
#endif

    j = 0;
    while(--j){          /* USB Reset by device only required on Watchdog Reset */
        i = 0;
        while(--i)
        	wdt_reset();
    }

#ifdef USB_CFG_PULLUP_IOPORT
    usbDeviceConnect();
#else
    USBDDR    = 0;      /*  remove USB reset condition */
#endif
}


int main(void)
{

    odDebugInit();
    hardwareInit();
    usbInit();

    sei();
    for(;;){    /* main event loop */
        wdt_reset();
        usbPoll();

        /*	host <= device : Response   */
		if( usbInterruptIsReady() && iwptr ) {
	        usbSetInterrupt(rx_buf+4-iwptr, iwptr);
			iwptr	= 0;
		}
    }

    return 0;
}

