
//   CDC-IO Uitlity routines for C#
//
//       Osamu Tamura @ Recursion Co., Ltd.

using System;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Globalization;


public class CDCIO : SerialPort
{

	/*  Open COM device						*/
    public bool Open(string num)
    {
        PortName = "COM" + num;
        ReadTimeout = 500;
        WriteTimeout = 500;
        try {
			Open();
		}
		catch {
			return false;
		}
		return IsOpen;
	}

    /*	Obtain device name					*/
    public string Who()
    {
		WriteLine( "@" );
		return ReadLine();
	}

    /*  Obtain SFR value					*/
    public int Get(M8 addr)
    {
		WriteLine( String.Format( "{0:x2} ?", (int)addr ) );
		return Int32.Parse(ReadLine(), NumberStyles.HexNumber);
	}

    /*  Set value to SFR					*/
    public void Set(M8 addr, int data)
    {
        WriteLine(String.Format("{0:x2} {1:x2} =", data & 0xff, (int)addr));
		ReadLine();
	}

    public void SetAnd(M8 addr, int data)
    {
        WriteLine(String.Format("{0:x2} {1:x2} &", data & 0xff, (int)addr));
		ReadLine();
	}

    public void SetOr(M8 addr, int data)
    {
        WriteLine(String.Format("{0:x2} {1:x2} |", data & 0xff, (int)addr));
		ReadLine();
	}

    public void SetXor(M8 addr, int data)
    {
        WriteLine(String.Format("{0:x2} {1:x2} ^", data & 0xff, (int)addr));
		ReadLine();
	}

    /*  Set two values to the same SFR		*/
    public void Set2(M8 addr, int data1, int data2)
    {
        WriteLine(String.Format("{0:x2} {1:x2} {2:x2} $", data2 & 0xff, data1 & 0xff, (int)addr));
		Thread.Sleep( 3 );
		ReadLine();
	}
}

