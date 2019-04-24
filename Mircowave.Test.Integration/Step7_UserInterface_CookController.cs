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
using NUnit.Framework.Internal;

namespace Mircowave.Test.Integration
{

    [TestFixture]
    public class Step7_UserInterface_CookController
    {
        private UserInterface _sut;
        private Door _door;
        private CookController _cookController;
        private Timer _timer;
        private Display _display;
        private Light _light;
        private PowerTube _powerTube;
        private Button _powerButton;
        private Button _timerButton;
        private Button _startCancelButton;
        private IOutput _fakeOutput;

        [SetUp]
        public void SetUp()
        {
            _fakeOutput = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_fakeOutput);
            _display = new Display(_fakeOutput);
            _light = new Light(_fakeOutput);
            _powerButton = new Button();
            _timerButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _timer = new Timer();
            _cookController = new CookController(_timer,_display,_powerTube);
            _sut = new UserInterface(_powerButton,_timerButton,_startCancelButton,_door,_display,_light,_cookController);
            _cookController.UI = _sut;
        }

        [Test]
        public void Test_UI_Calls_CookController_WithTStartCooking()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();

            _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));

        }

        [Test]
        public void Test_UI_CallsStop_OnCookController_DueToOpenedDoor()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();
            _door.Open();

            _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));

        }

        [Test]
        public void Test_UI_CallsStop_OnCookController_DueToPressedStartCancel()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));

        }
    }
}
