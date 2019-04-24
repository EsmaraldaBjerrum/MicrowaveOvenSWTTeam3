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
    class Step4_Button_UI
    {
        private Button _sutPower;
        private Button _sutTimer;
        private Button _sutSC;
        private IDoor _fakeDoor;
        private UserInterface UI;
        private ICookController _fakeCookController;
        private IDisplay _fakeDisplay;
        private ILight _fakeLight;
        private IPowerTube _fakePowerTube;

        [SetUp]
        public void SetUp()
        {
            _fakeDoor = Substitute.For<IDoor>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLight = Substitute.For<ILight>();
            _fakeCookController = Substitute.For<ICookController>();
            _sutPower = new Button();
            _sutTimer = new Button();
            _sutSC = new Button();
            _fakePowerTube = Substitute.For<IPowerTube>();
            UI = new UserInterface(_sutPower, _sutTimer, _sutSC, _fakeDoor, _fakeDisplay, _fakeLight,
                _fakeCookController);
        }

        [Test]
        public void Test_sutPower_UI()
        {
            _sutPower.Press();
            _fakeDisplay.Received().ShowPower(50);
        }

        

        [Test]
        public void Test_sutTimer_UI_DisplayShowTime()
        {

            _sutPower.Press();
            _sutTimer.Press();
            _fakeDisplay.Received().ShowTime(Arg.Is<int>(1), Arg.Is<int>(0));
        }

        [Test]
        public void Test_sutSC_UI_LightTurnOn()
        {
            _sutPower.Press();
            _sutTimer.Press();
            _sutSC.Press();
            _fakeLight.Received(1).TurnOn();
        }

        [Test]
        public void Test_sutSC_UI_CookControllerStartCooking()
        {
            _sutPower.Press();
            _sutTimer.Press();
            _sutSC.Press();
            _fakeCookController.Received(1).StartCooking(50, 60);
        }

        [Test]
        public void Test_sutSC_UI_CookControllerStop()
        {
            _sutPower.Press();
            _sutTimer.Press();
            _sutSC.Press();
            _sutSC.Press();
            _fakeCookController.Received(1).Stop();
        }

        [Test]
        public void Test_sutSC_UI_DisplayClear()
        {
            _sutPower.Press();
            _sutTimer.Press();
            _sutSC.Press();
            _sutSC.Press();
            _fakeDisplay.Received(2).Clear();
        }

        [Test]
        public void Test_sutSC_UI_LightOff()
        {
            _sutPower.Press();
            _sutTimer.Press();
            _sutSC.Press();
            _sutSC.Press();
            _fakeLight.Received(1).TurnOff();
        }

    }
}
