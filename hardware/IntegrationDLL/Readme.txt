

                                    CDC-IO Sample


    This is the Readme file to CDC-IO sample programs. For more information
    please visit
    http://www.recursion.jp/avrcdc/


SUMMARY
=======
    The CDC-IO performs the CDC (Communication Device Class) connection over
    low-speed USB. It provides universal access to the AVR's internal
    functions thorough virtual COM port.
    The CDC-IO and its sample programs are developed by
    Osamu Tamura @ Recursion Co., Ltd.


SPECIFICATION
=============
    [Files]
        „   
        „   License.txt
        „   Readme.txt
        „   cdcio-demo.pdf            Schematic of the test circuit
        „   
        „¥„Ÿc_basic
        „   „   atmega48.h            ATmega48-168, SFR definition
        „   „   atmega8.h             ATmega8-16,   SFR definition
        „   „   attiny2313.h
        „   „   attiny44.h
        „   „   attiny45.h
        „   „   attiny461.h
        „   „   iodemo.c              CDC-IO demo
        „   „   iodemo.sln
        „   „   iodemo.vcproj
        „   „   
        „   „¤„ŸRelease
        „           iodemo48-2.exe    ATmega48-168, TEST-2
        „           iodemo48-3.exe    ATmega48-168, TEST-3
        „           iodemo48.exe      ATmega48-168, TEST-1
        „           iodemo8-2.exe     ATmega8-16,   TEST-2
        „           iodemo8-3.exe     ATmega8-16,   TEST-3
        „           iodemo8.exe       ATmega8-16,   TEST-1
        „           
        „¥„Ÿc_mac
        „       atmega48.h
        „       atmega8.h
        „       attiny2313.h
        „       attiny44.h
        „       attiny45.h
        „       attiny461.h
        „       iodemo.c
        „       Makefile
        „       
        „¥„Ÿnet2005                   .NET 2005 projects
        „   „¥„Ÿcpp
        „   „   „   
        „   „   „¤„ŸRelease
        „   „           
        „   „¥„Ÿcs
        „   „   „   iodemo.sln
        „   „   „   
        „   „   „¤„Ÿiodemo
        „   „       „   
        „   „       „¥„Ÿbin
        „   „       „   „¤„ŸRelease
        „   „       „           
        „   „       „¤„ŸProperties
        „   „               
        „   „¤„Ÿvb
        „       „   iodemo.sln
        „       „   
        „       „¤„Ÿiodemo
        „           „   
        „           „¥„Ÿbin
        „           „   „¤„ŸRelease
        „           „           
        „           „¤„ŸMy Project
        „                   
        „¤„Ÿvs2003                    Visual Studio 2003 projects
                „¥„Ÿc
                „   „   
                „   „¤„ŸRelease
                „           
                „¥„Ÿcpp
                „   „   
                „   „¤„ŸRelease
                „           
                „¤„Ÿvb
                        „   
                        „¤„Ÿbin
                            

    These programs are designed to show the basic method to access COM port
    from your program. They also provide wrapper functions to ease using
    CDC-IO devices.

    Include "attiny**.h/cs/vb" or "atmega**.h/cs/vb" into your project to
    access AVR's Special function registers (SFR). You can use register
    name instead of register address.

    These programs are for CDC-IO ATmega8/48/88/168 versions. The code is
    common for ATmega48/88/168, and some registers are missing in ATmega8.
    All the command sequence should be modified for other AVR's.

    The interrupt vector number is reported when invoked. This mechanism
    is a bit hard to use. There is a simple sample in "c_basic/".
    It may be easier to poll and check the interrupt flag status from the
    host.

    EEPROM write sequence needs some modification.
    For practical use, the "read" procedure from the device should be
    implemented as non-blocking and retry mechanism.

USAGE
=====
    [Windows XP/2000/Vista]
    1. Make the test circuit according to the schematic.
    2. Connect CDC-IO device. Install the driver.
       (Read the instruction in "avrcdc_inf.zip" carefully.)
    3. Open "Command Prompt" window and invoke
       iodemo* #
       (# is the COM port number).
    4. Tune the volume.
    5. hit any key to quit.

    c_basic/iodemo*-2 is a sample to access EEPROM. It only uses the first
    256 bytes.

    c_basic/iodemo*-3 is a sample to test the interrupt function. Connect
    PB0/1 and Gnd to generate Pin Change interrupt.

    [Mac OS X]
    There is a simple sample for Macintosh. "Make" the application in
    c_mac/ and invoke it with the CDC device name. It is like
    /dev/tty.usbmodem* (* is some hexadecimal numbers). Execute
    iodemo *
    Hit "return" or "enter" to quit.

    [Linux 2.4]
    Refer to the Macintosh sample. It may need some modification.


DEVELOPMENT
===========
    Samples are developed using Microsoft Visual Studio 2003 and 2005.
    The 2005 .NET has a library class to access COM device, and makes the
    code simpler.

    You can use any programing language if it has COM port interface.


USING CDC-IO FOR FREE
======================
    The CDC-IO sample programs are published under an Open Source compliant
    license. See the file "License.txt" for details.


    Osamu Tamura @ Recursion Co., Ltd.
    http://www.recursion.jp/avrcdc/
    15 July 2009

