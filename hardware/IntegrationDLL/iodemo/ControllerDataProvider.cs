using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AssemblyCSharp
{
    public class ControllerDataProvider : IHeartDataProvider
    {
        private static readonly BeatValidator beat = new BeatValidator();
        private static readonly CDCIO com = CDCIO.getInstance();

        private class InnerADCFetcher
        {
            public void refreshAdc()
            {
                while (true)
                {
                    beat.updateADCValue(readADC5());
                    Thread.Sleep(5);
                }
            }
        }

        private Thread adcFetchThread;

        public ControllerDataProvider()
        {
            setupADC();

            // All of port B is output
            com.Set(M8.DDRB, 0x3F);
            // PC0 is also output
            com.SetOr(M8.DDRC, 0x1);
            // Turn off any leds that are on
            com.Set(M8.PORTB, 0);

            InnerADCFetcher adcFetcher = new InnerADCFetcher();
            adcFetchThread = new Thread(new ThreadStart(adcFetcher.refreshAdc));
            adcFetchThread.Start();
        }

        public int GetCurrentRate()
        {
            return beat.getBPM();
        }

        private void setupADC()
        {
            com.Set(M8.DDRC, 0); // all C is input
            com.Set(M8.PORTC, 0);
            // ADCSRA = (1<<ADEN) | (1<<ADPS2) | (1<<ADPS0);
            int adcsra = (1 << 7) | (1 << 6) | (1 << 2) | (1 << 0) | (1 << 5); // (1<<5) is AD Freerun!!!!
            com.Set(M8.ADMUX, (1 << 6) | 0x05); //set PC5 as ADC, VRef = VCC
            com.Set(M8.ADCSRA, adcsra);
        }

        private static int readADC5()
        {
            return (com.Get(M8.ADCH) << 8) | com.Get(M8.ADCL);
        }


        public void setLeds(bool[] ledStates)
        {
            int mask = 0;
            for (int i = 0; i < 6; i++)
            {
                if(ledStates[i])
                    mask |= 1 << i;
            }
            com.Set(M8.PORTB, mask);
        }

        public void setVibration(bool on)
        {
            com.Set(M8.PORTC, on ? 1 : 0);
        }
    }
}
