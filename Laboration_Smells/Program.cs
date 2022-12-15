using Laboration_Smells;


IOHelper _ioHelper = ConsoleHelper.GetInstance();
GameEngine gameEngine = new(_ioHelper);

gameEngine.RunGame(_ioHelper);
