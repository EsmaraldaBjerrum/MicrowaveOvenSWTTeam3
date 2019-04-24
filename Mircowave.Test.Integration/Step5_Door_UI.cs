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
   class Step5_Door_UI
   {
      private Button _bPower;
      private Button _bTimer;
      private Button _bStart;
      private Door _sut;
      private UserInterface _ui;
      private ICookController _fakeCookController;
      private IDisplay _fakeDisplay;
      private ILight _fakeLight;

      [SetUp]
      public void SetUp()
      {
         _bPower = new Button();
         _bTimer = new Button();
         _bStart = new Button();
         _sut = new Door();
         _fakeDisplay = Substitute.For<IDisplay>();
         _fakeCookController = Substitute.For<ICookController>();
         _fakeLight = Substitute.For<ILight>();
         _ui = new UserInterface(_bPower,_bTimer,_bStart,_sut,_fakeDisplay,_fakeLight,_fakeCookController);
      }

      [Test]
      public void Test_sut_doorOpened_LightOn()
      {
         _sut.Open();
         _fakeLight.Received().TurnOn();
      }

      [Test]
      public void Test_sut_doorClosed_LightOff()
      {
         _sut.Open();
         _sut.Close();
         _fakeLight.Received().TurnOff();
      }

      [Test]
      public void Test_sut_doorOpenWhileCooking_CookControllerStopped()
      {
         _bPower.Press();
         _bTimer.Press();
         _bStart.Press();
         _sut.Open();
         _fakeCookController.Received(1).Stop();
      }
   }
}
