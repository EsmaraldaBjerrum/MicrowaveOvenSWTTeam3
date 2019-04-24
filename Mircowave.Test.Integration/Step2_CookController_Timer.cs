using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Mircowave.Test.Integration
{
   [TestFixture]
   class Step2_CookController_Timer
   {
      private Display _display;
      private PowerTube _powerTube;
      private CookController _sut;
      private IOutput _fakeOutput;
      private Timer _timer;
      private IUserInterface _fakeUI;


      [SetUp]
      public void SetUp()
      {
         _fakeOutput = Substitute.For<IOutput>();
         _timer = new Timer();
         _display = new Display(_fakeOutput);
         _powerTube = new PowerTube(_fakeOutput);
         _sut = new CookController(_timer, _display, _powerTube);
         _fakeUI = Substitute.For<IUserInterface>();
         _sut.UI = _fakeUI;
      }

      [Test]
      public void Test_SUT_To_Timer_Start()
      {
         ManualResetEvent pause = new ManualResetEvent(false);

         _sut.StartCooking(50,1);
         pause.WaitOne(3000);
         _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:00")));

          _timer.Stop();
      }

      [Test]
      public void Test_SUT_To_Timer_Stop()
      {
        
         _sut.StartCooking(50,50);
         _sut.OnTimerExpired(this,EventArgs.Empty);
         _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));

         _timer.Stop();
         
      }

      [Test]
      public void Test_SUT_To_Powertube_TurnOffByEvent()
      {
          ManualResetEvent pause = new ManualResetEvent(false);

          _sut.StartCooking(50, 1);
          pause.WaitOne(3000);
          _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));

          _timer.Stop();
        }
      
   }
}
