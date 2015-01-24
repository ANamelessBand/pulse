using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyCSharp
{
    public class FeedbackBracelet
    {
        private static readonly CDCIO com = CDCIO.getInstance();

        public FeedbackBracelet()
        {
            // All of port B is output
            com.Set(M8.DDRB, 0x3F);
            // PC0 is also output
            com.SetOr(M8.DDRC, 0x1);
            // Turn off any leds that are on
            com.Set(M8.PORTB, 0);
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
