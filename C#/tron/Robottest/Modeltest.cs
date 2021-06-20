using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElszabadultRobot.Model;
namespace Robottest
{
    [TestClass]
    public class Modeltest
    {
        int n = 7;
        int testx;
        int testy;
        private GameModel _model;
        [TestInitialize]
        public void Initialize()
        {
            _model = new GameModel();
            _model.Mapgenerator(n);


        }
        [TestMethod]
        public void Generator()
        {
           
            
            Assert.AreEqual(_model.map.grid.Length, n*n);
            Assert.AreEqual(_model.map.Y1, n / 2);
            Assert.AreEqual(_model.map.X1, 0);
            bool palyanvan = true;
            for (int i = 0; i < 4; i++)
            {
                if (i == 1)
                {
                    palyanvan = false;
                }
            }
            //Assert.IsTrue(palyanvan);
        }
        [TestMethod]
        public void Lepes()
        {
            _model.map.X1 = 2;
            _model.map.Y1 = 2;

            _model.map.Direction1 = 0;
            _model.Step();
            Assert.AreEqual(1,_model.map.X1);

            _model.map.Direction1 = 1;
            _model.Step();
            Assert.AreEqual(3, _model.map.Y1);

            _model.map.Direction1 = 2;
            _model.Step();
            Assert.AreEqual(2, _model.map.X1);

            _model.map.Direction1 = 3;
            _model.Step();
            Assert.AreEqual(2, _model.map.Y1);

        }
       
    }
}
