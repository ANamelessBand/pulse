
/*
	iodemo.cs : CDC-IO demo

	O.Tamura @ Recursion Co., Ltd.
*/

using System;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Globalization;


namespace iodemo
{
    class Program
    {

        private static CDCIO com = CDCIO.getInstance();

        public static void beater()
        {
            BeatValidator beat = new BeatValidator();

            setupADC();
            for (int i = 0; i < 20000; i++)
            {
                Thread.Sleep(5);
                int adcv = readADC5(com);
                beat.updateADCValue(adcv);
                if (beat.getQS())
                    Console.WriteLine("BEAT! Loopstamp: " + i);
            }
        }

        public static int Main(string[] args)
        {
            
            //AssemblyCSharp.ControllerDataProvider cdp = new AssemblyCSharp.ControllerDataProvider();
            while(false)
            {
                //Console.WriteLine(cdp.GetCurrentRate());
                Thread.Sleep(1000);
            }


            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
                Console.WriteLine("   {0}", s);

            beater();


            AssemblyCSharp.FeedbackBracelet bracelet = new AssemblyCSharp.FeedbackBracelet();
            while (true)
            {
                bracelet.setVibration(true);
                Thread.Sleep(1000);
                bracelet.setVibration(false);
                Thread.Sleep(1000);
            }


            return 0;
        }

        /*
         * #define ADEN	7
#define ADSC	6
#define ADFR	5
#define ADIF	4
#define ADIE	3
#define ADPS2	2
#define ADPS1	1
#define ADPS0	0
         * */
        private static int readADC5(CDCIO com)
        {
            return (com.Get(M8.ADCH)<<8) | com.Get(M8.ADCL);
        }

        private static void setupADC()
        {
            com.Set(M8.DDRC, 0); // all C is input
            com.Set(M8.PORTC, 0);
            // ADCSRA = (1<<ADEN) | (1<<ADPS2) | (1<<ADPS0);
            int adcsra = (1 << 7) | (1<<6) | (1 << 2) | (1 << 0) | (1 << 5); // (1<<5) is AD Freerun!!!!
            com.Set(M8.ADMUX, (1 << 6) | 0x05); //set PC5 as ADC, VRef = VCC
            com.Set(M8.ADCSRA, adcsra);
        }

    }
}


