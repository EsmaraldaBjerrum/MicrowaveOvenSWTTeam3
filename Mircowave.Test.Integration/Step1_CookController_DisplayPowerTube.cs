using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microwave.Application;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;


namespace Mircowave.Test.Integration
{
   [TestFixture]
    public class Step1_CookController_DisplayPowerTube
   {
       private Display _display;
       private PowerTube _powerTube;
       private CookController _sut;
       private IOutput _fakeOutput;
       private ITimer _fakeTimer;
       private IUserInterface _fakeUserInterface;
       
        
        [SetUp]
       public void SetUp()
        {
            _fakeUserInterface = Substitute.For<IUserInterface>();
           _fakeOutput = Substitute.For<IOutput>();
           _fakeTimer = Substitute.For<ITimer>();
            _display = new Display(_fakeOutput);
           _powerTube = new PowerTube(_fakeOutput);
           _sut = new CookController(_fakeTimer,_display,_powerTube,_fakeUserInterface);
       }

       [Test]
       public void Test_SUT_To_Display_Showtime()
       {
          _fakeTimer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

           //Nedenfor er to forskellige måder at gøre det samme...
           _fakeOutput.Received(1).OutputLine(Arg.Any<string>());
           _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains(":")));

           _fakeTimer.Stop();
           
       }

       [Test]
       public void Test_SUT_To_Powertube_TurnOn()
       {
           _sut.StartCooking(50,50);
           _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("7")));
           _fakeTimer.Stop();
       }

      
       [Test]
       public void Test_SUT_To_Powertube_TurnOffByStop()
       {
           _sut.StartCooking(50,50);
           _sut.Stop();
           _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
           _fakeTimer.Stop();
       }

       [Test]
       public void Test_SUT_To_Powertube_TurnOffByExpiredEvent()
       {
           _sut.StartCooking(50,50);
           _fakeTimer.Expired += Raise.EventWith(this, EventArgs.Empty);
           _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));

           _fakeTimer.Stop();
       }
    }
}
