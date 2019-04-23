using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
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


        [SetUp]
        public void SetUp()
        {
            _fakeOutput = Substitute.For<Output>();
            _display = new Display(_fakeOutput);
            _powerTube = new PowerTube(_fakeOutput);
            _sut = new Timer();
            _cookController = new CookController(_sut, _display, _powerTube);
        }

        [Test]
        public void Test_OnTimerExpired_CookControllerRecieveEvent()
        {
            _sut.Expired += Raise.EventWith(this, EventArgs.Empty);

            //Assert.That();
        }
    }
}
