using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;

namespace GRBL_Arduino_Communication
{
    public partial class Form1 : Form
    {
        string[] lines = System.IO.File.ReadAllLines(@"F:\Desktop\Boxish G code.txt");
        // ^ Turns the text file into a string array
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i <= lines.Length - 1; i++)
            {
                Console.WriteLine(lines[i]);
            }
            //^ this was to test whether or not the string was saved as an array correctly
            //   it'll print on the output display when you hit start
        }
        SerialPort port;
        private void Form1_Load(object sender, EventArgs e)
        {


        }
        private void button1_Click(object sender, EventArgs e) // this is the "connect" button
                                                               //  DO NOT PRESS THIS BUTTON WITHOUT THE ARDUINO!!  Nothing will happen, and errors will pop up
        {
            port = new SerialPort(textBox1.Text, int.Parse(textBox2.Text), Parity.None, 8, StopBits.One);
            // ^ Initializes the port.  [new SerialPort("COM#", 115200, Parity.None, 8, StopBits.One);] <- general form for project
            port.Open();
            // ^ opens serial port.  can't interact with it if it isn't opened
            port.Write("x1 y1 z0.5" + "\n");
            // ^ sends this to the serial monitor, and motors go to the position (1, 1, 0.5)
            Thread.Sleep(2000);
            // ^ a delay for debugging
            port.Write("x0 y0 z0" + " \n");
            // ^ another line of G code for debugging
            port.Close();
            // ^ closes the serial port
        }
        private void button2_Click(object sender, EventArgs e) // This is the " send " button
                                                               // Just don't hit it without the Arduino and motors
        {
            port.Open();
            int arrayLength = lines.Length;
            for (int i = 0; i <= arrayLength - 1; i++)
            {
                port.Write(lines[i] + "\n");
                Thread.Sleep(2000);
                // This kinda shows how it can receive the text file line by line
            }

            port.Close();
        }
    }
}
