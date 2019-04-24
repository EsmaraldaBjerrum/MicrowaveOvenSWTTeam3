using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Mircowave.Test.Integration
{
   [TestFixture]
   class Step8_Cookcontroller_UserInterface
   {
      private Button _bPower;
      private Button _bTimer;
      private Button _bStart;
      private Door _door;
      private IUserInterface _UI;
      private CookController _sut;
      private Timer _timer;
      private Display _display;
      private Light _light;
      private PowerTube _powerTube;
      private IOutput _fakeOutput;

      [SetUp]
      public void SetUp()
      {
         _bStart = new Button();
         _bPower = new Button();
         _bTimer = new Button();
         _door = new Door();
         _timer = new Timer();
         _fakeOutput = Substitute.For<IOutput>();
         _display = new Display(_fakeOutput);
         _powerTube = new PowerTube(_fakeOutput);
         _light = new Light(_fakeOutput);
         _sut = new CookController(_timer,_display,_powerTube);
         _UI = new UserInterface(_bPower,_bTimer,_bStart,_door,_display,_light,_sut);

         _sut.UI = _UI;
      }

      [Test]
      public void Test_sutCookingIsDone_DisplayClear()
      {
         ManualResetEvent pause = new ManualResetEvent(false);
         _sut.StartCooking(50,1);
         pause.WaitOne(1100);
         _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
      }
   }
}
