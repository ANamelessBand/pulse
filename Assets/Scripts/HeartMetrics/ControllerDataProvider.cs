using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AssemblyCSharp
{
    class ControllerDataProvider : IHeartDataProvider
    {
        private static readonly String COMPORTN = "3";

        private static readonly BeatValidator beat = new BeatValidator();
        private static readonly CDCIO com = new CDCIO();

        private class InnerADCFetcher
        {
            public void refreshAdc()
            {
                while (true)
                {
                    beat.updateADCValue(readADC5());
                }
            }
        }

        private Thread adcFetchThread;

        public ControllerDataProvider()
        {
            if (!com.Open(COMPORTN))
            {
                throw new Exception("COM"+COMPORTN+" not available.");
            }
            setupADC();

            InnerADCFetcher adcFetcher = new InnerADCFetcher();
            adcFetchThread = new Thread(new ThreadStart(adcFetcher.refreshAdc));
            adcFetchThread.Start();
        }

        public int GetCurrentRate()
        {
            // ugly hack
            int r =  beat.getBPM()/2;
            if (r <55)
                return r*2;
            return r;
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
    }
}
