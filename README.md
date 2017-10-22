# BloomComponent
Add bloom effect to MonoGame projects

# How to use:

1.Add the Nuget package to your solution: https://www.nuget.org/packages/BloomComponent/

2. In the MonoGame Pipeline tool for your project, add the 3 shaders that were installed with the Nuget package.

3.Initialize the BloomComponent object (note: not actually a MonoGame component):
```
var bloom = new BloomComponent();
bloom.LoadContent(spritebatch, content, graphicsDevice);
```

4. Call the BeginDraw method before your draw loop, and the Draw method after the end of the draw loop:
```
bloom.BeginDraw();

spritebatch.Begin();

//...draw all your stuff

spritebatch.End();

bloom.Draw(gameTime);
```

5. There are a couple of preset bloom settings you can try out:
```
bloom.Settings = BloomSettings.PresetSettings[6]; //pick a number 0-6
```
```
//					Name			Thresh	Blur	Bloom	Base	BloomSat	BaseSat
new BloomSettings(	"danno",		0.5f,	1,		1.0f,	1,		1,			1),
new BloomSettings(	"Default",		0.25f,	4,		1.25f,	1,		1,			1),
new BloomSettings(	"Subtle",		0.5f,	2,		1,		1,		1,			1),
new BloomSettings(	"Soft",			0,		3,		1,		1,		1,			1),
new BloomSettings(	"Desaturated",	0.5f,	8,		2,		1,		0,			1),
new BloomSettings(	"Saturated",	0.25f,	4,		2,		1,		2,			0),
new BloomSettings(	"Blurry",		0,		2,		1,		0.1f,	1,			1)
```