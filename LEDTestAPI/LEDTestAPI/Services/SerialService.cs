using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;

namespace LEDTestAPI.Services
{
   public class SerialService
   {

      #region Fields

      private readonly SerialPort _arduino;

      #endregion

      /// <summary>
      /// Initializes an instance of class <see cref="SerialService"/>
      /// </summary>
      public SerialService()
      {
         _arduino = new SerialPort();
         _arduino.BaudRate = 115200;
         _arduino.PortName = "COM7";
         _arduino.Open();
      }

      ~SerialService()
      {
         _arduino.Close();
      }

      public bool IsOpen => _arduino.IsOpen;

      public void SendText(string text)
      {
         _arduino.Write(text);
      }


   }
}