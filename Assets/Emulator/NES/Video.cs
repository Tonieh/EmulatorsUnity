// created on 10/24/2004 at 11:51
using System;
using System.Runtime.InteropServices;
using System.Threading;

public class Video
{
	int	bpp = 16;
	int	width = 256;
	int	height = 224;
	DateTime dtbefore, dtafter;
	int fps;
	int framecount;
	PPU myPPU;
	
	bool willSleep;
	int sleepTime;
	
	public void BlitScreen()
	{
		//Marshal.Copy(myPPU.offscreenBuffer, 256*8, myPPU.currentScreen, width*height);

		//FIXME: Come up with a better way of frame limiting
				
		//Sdl.SDL_Delay((int)(16.667 - ((dtafter-dtbefore).Ticks / 10000.0)));
		//Console.WriteLine("Will Delay: {0}", 60 * ((dtafter-dtbefore).Ticks / 1000000.0));
		framecount++;
		if ((framecount % 100) == 0)
		{
			//FIXME: This assumes NTSC
			
			dtafter = DateTime.Now;
			fps = (int)(((dtafter-dtbefore).Ticks) / 100000);
			//Console.WriteLine("Current Speed: {0}", 100.0 / fps);
			if (fps < 100)
			{
				willSleep = true;
				sleepTime++;
				//Console.WriteLine("Will Delay: {0}", (100 - fps) * 1000);
			}
			dtbefore = DateTime.Now;
		}
		if (willSleep)
		{
			Thread.Sleep(sleepTime);
		}
		//Thread.Sleep(10);
	}
	
	public Video(PPU thePPU)
	{
		//Initialize video emulation framework
		myPPU = thePPU;
		//END Initialize video emulation framework
	}
	
	public void ToggleFullscreen()
	{
	}

	public void StartVideo()
	{		
		dtbefore = DateTime.Now;
		framecount = 0;
		
		willSleep = false;
		sleepTime = 0;
	}
	
	public void CloseVideo()
	{
	}
}
