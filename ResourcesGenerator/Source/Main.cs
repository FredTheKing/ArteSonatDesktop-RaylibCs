using ResourcesGenerator.Source.Packages;

ResourceGenerator generator = new ResourceGenerator();

//generator.AddResource("TestCodeImage", "Resources/photo.png");
generator.AddResource("TestCodePanorama", "Resources/panorama.png");
generator.AddResource("TestCodeLBP", "Resources/lbp2.png");
generator.AddResource("TestCodekarting", "Resources/karting.png");

generator.ExecuteGeneration();
Console.WriteLine("Successful Generation!");