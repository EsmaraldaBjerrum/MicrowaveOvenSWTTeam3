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
    class Step6_UI_DisplayLight
    {

        private Button _bPower;
        private Button _bTimer;
        private Button _bSC;
        private Door _door;
        private UserInterface sut;
        private Display _display;
        private Light _light;
        private IOutput _fakeOutput;
        private ICookController _fakeCookController;

        [SetUp]
        public void SetUp()
        {
            _fakeOutput = Substitute.For<IOutput>();
            _bPower = new Button();
            _bTimer = new Button();
            _bSC = new Button();
            _door = new Door();
            _fakeCookController = Substitute.For<ICookController>();
            _display = new Display(_fakeOutput);
            _light = new Light(_fakeOutput);
            sut = new UserInterface(_bPower, _bTimer, _bSC, _door, _display, _light, _fakeCookController);

        }

        [Test]
        public void Test_sut_Display_TurnOn()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);

            _fakeOutput.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
            
        }
        
    }
}
