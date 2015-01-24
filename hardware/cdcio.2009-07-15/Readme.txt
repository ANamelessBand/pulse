

                                    CDC-IO


    This is the Readme file to firmware-only CDC driver for Atmel AVR
    microcontrollers. For more information please visit
    http://www.recursion.jp/avrcdc/


SUMMARY
=======
    The CDC-IO performs the CDC (Communication Device Class) connection over
    low-speed USB. It provides universal access to the AVR's internal
    functions thorough virtual COM port.
    The CDC-IO is developed by Osamu Tamura @ Recursion Co., Ltd.


SPECIFICATION
=============
    Send text command to read/modify port bits. Use any terminal software
    or your own program.

    CDC-IO parallel port: 3/13/18 bits
    (ATtiny2313)
        PORTB: bit 0-7
        PORTD: bit 0,1,4-6
    (ATtiny45/85)
        PORTB: bit 0-2
    (ATtiny461/861)
        PORTA: bit 0-7
        PORTB: bit 0-2,4,5
    (ATmega8/48/88/168)
        PORTB: bit 0-5
        PORTC: bit 0-5
        PORTD: bit 0,1,4-7

    CDC-IO terminal settings
        speed:    any
        datasize: 7/8 bit
        parity:   any
        stopbit:  any

      Although RS-232C setting is not matter, set data size to 8bit to avoid
      unexpected troubles. To see the requested command, turn on "local echo".

    CDC-IO command
        function       command       format                response
        ------------------------------------------------------------------
        Who            @             @                     "cdc-io", CR-LF
     *  Get            ?             addr ?                data, CR-LF
     *  Set            =             data addr =           CR-LF
        AND & Set      &             data addr &           CR-LF
        OR & Set       |             data addr |           CR-LF
     *  EX-OR & Set    ^             data addr ^           CR-LF
        Set Double     $             data2 data1 addr $    CR-LF

        addr:      memory mapped SFR address in hex
        data:      8 bit data in hex
        delimiter: Tab, Space, CR, LF
        *: ATtiny2313 supports these commands only.

        predefined addr: (case-insensitive, NOT supported at ATtiny2313)
            PINA, DDRA, PORTA
            PINB, DDRB, PORTB
            PINC, DDRC, PORTC
            PIND, DDRD, PORTD

        example: ('_' means delimiter)
          DDRB_?_      Replies DDRB value with CR-LF.
          12_34_=_     Write 0x12 to address:0x34, replies CR-LF.
          FB_PORTC_&_  Write (PORTC & 0xFB) to PORTC, replies CR-LF.
	(ATtiny2313)
          36_?_        Replies PINB value with CR-LF.
          12_37_=_     Write 0x12 to DDRB, replies CR-LF.
          FB_38_^_     Write (PORTB ^ 0xFB) to PORTB, replies CR-LF.

    Previous data and addr can be reused. Just enter command char to repeat.

    USB line is connected to PORTB/PORTD. Avoid changing these bits.
    Use '&', '|' or '^' to modify the port direction. Use PIN* to toggle
    bits if the port is assigned to output.

    The interrupt vector number is reported when invoked.
    The vector number format is "\?? CR-LF" (? is a hexadecimal number). 
    See the code sample (c_basic) for details.
        example for ATtiny461
          04_53_=_     Set Timer0 prescaler to 1/256 (TCCR0B).
          80_35_=_     Set Timer0 counter to 16 bit mode (TCCR0A).
          02_59_=_     Enable Timer0 overflow interrupt (TIMSK).

    USB bus-powered Vcc is about 3.6V. Be careful of interfacing to the higher
    voltage circuit. Usable bus-powered current is about 80mA.

    Any SFRs(special function registers) are accessible. See AVR datasheet to
    use Timers, ADC, EEPROM, etc. 

    Internal RC Oscillator is calibrated at startup time. It may be unstable 
    after a long time operation (ATtiny45/461).

    Although the CDC is supported by Windows 2000/XP/Vista, Mac OS 9.1/X,
    and Linux 2.4, low-speed bulk transfer is not allowed by the USB standard.
    Use CDC-IO at your own risk.


USAGE
=====
    [Windows XP/2000/Vista]
    Download "avrcdc_inf.zip" and read the instruction carefully.
 
    [Mac OS X]
    You'll see the device /dev/cu.usbmodem***.

    [Linux]
    The device will be /dev/ttyACM*.
    Linux 2.6 does not accept low-speed CDC without patching the kernel.


DEVELOPMENT
===========
    Build your circuit and write firmware (cdcio*.hex) into it.
    C1:104 means 0.1uF, R3:1K5 means 1.5K ohms.

    If the connection is unstable, add another diode after D2, or try other
    USB-Hub or PC.

    This driver has been developed on AVR Studio 4.16 and WinAVR 20090313.
    If you couldn't invoke the project from cdcio.aps, create new GCC project
    named "cdcio" under "cdcio****-**-**/" without creating initial file. 
    Select each Makefile at "Configuration Options".

    Fuse bits
                          ext  H-L
        ATtiny2313         FF CD-FF
        ATtiny45/85        FF CE-F1
        ATtiny461/861      FF C8-F1
        ATmega8               8F-FF
        ATmega48/88/168    FF CE-FF

	SPIEN=0, WDTON=0, CKOPT(mega8)=0,
	Crystal: Ex.8MHz/PLL(45,461), BOD: 1.8-2.7V

    The code size of CDC-IO is 2.0-3.6KB, and 128B RAM is required at least.


USING CDC-IO FOR FREE
======================
    The CDC-IO is published under an Open Source compliant license.
    See the file "License.txt" for details.

    You may use this driver in a form as it is. However, if you want to
    distribute a system with your vendor name, modify these files and recompile
    them;
        1. Vendor String in usbconfig.h
        2. COMPANY and MFGNAME strings in avrcdc.inf/lowcdc.inf 



    Osamu Tamura @ Recursion Co., Ltd.
    http://www.recursion.jp/avrcdc/
    12 January 2007
    7 April 2007
    7 July 2007
    25 August 2008
    15 July 2009

