using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

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


      [SetUp]
      public void SetUp()
      {
         _fakeOutput = Substitute.For<IOutput>();
         _timer = new Timer();
         _display = new Display(_fakeOutput);
         _powerTube = new PowerTube(_fakeOutput);
         _sut = new CookController(_timer, _display, _powerTube);
      }

      [Test]
      public void Test_SUT_To_Timer_Start()
      {
         _sut.StartCooking(50,50);

         _timer.Start(50);
         Assert.That(_timer.TimeRemaining, Is.EqualTo(50));
      }

      [Test]
      public void Test_SUT_To_Timer_Stop()
      {
         _sut.Stop();

         _timer.Stop();
         //Assert.That(_timer., Is.EqualTo(false));
      }

      [Test]
      public void Test_SUT_To_Powertube_TurnOffByEvent()
      {
        _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
        //Assert.That(_timer.TimeRemaining, Is.EqualTo()
      }


   }
}
