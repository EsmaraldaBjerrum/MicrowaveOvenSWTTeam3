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
    class Step3_Timer_CookController
    {

        private Timer _sut;
        private CookController _cookController;
        private Display _display;
        private PowerTube _powerTube;
        private Output _fakeOutput;
       private IUserInterface _fakUserInterface;


        [SetUp]
        public void SetUp()
        {
            _fakeOutput = Substitute.For<Output>();
           _fakUserInterface = Substitute.For<IUserInterface>();
            _display = new Display(_fakeOutput);
            _powerTube = new PowerTube(_fakeOutput);
            _sut = new Timer();
            _cookController = new CookController(_sut, _display, _powerTube,_fakUserInterface);

           
        }

        [Test]
        public void Test_OnTimerExpired_CookControllerRecieveEvent()
        {
            _sut.Expired += Raise.EventWith(this, EventArgs.Empty);

            _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));

        }

        [Test]
        public void Test_OnTimerTick_To_Cookcontrol_ShowtimeIsCalled()
        {
            _sut.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains(":")));
        

        }
    }
}
