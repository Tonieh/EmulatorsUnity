// created on 10/24/2004 at 11:39
using System;

//The base class that defines how an engine works
public class EngineBase 
{	
	/* 
	   There is a sizeable handful of tasks the engine will have to accomplish.  In 
	   our case it's going to control the memory and cpu, attach itself to a video 
	   output, and load the cart into its memory.  Every console will need to be able 
	   to do all four of those tasks, so we abstract away what functions, at a minimum, 
	   each must provide. 
	*/

	// Our CPU functions
	// FIXME: should SetPC and GetPC type functions be here as well?
	public virtual void RunNextInstruction()
	{
		return;
	}
	
	// Our memory functions.  In the future these could be consolidated, but I've let
	// them separate for the sake of the slight speed gain that grants us
	// FIXME: these are commented out because of a ushort vs. uint issue
	/*
	public virtual byte ReadMemory8(uint address)
	{
		return 0;
	}
	
	public virtual ushort ReadMemory16(uint address)
	{
		return 0;
	}
	
	public virtual byte WriteMemory8(uint address, byte data)
	{
		return 0;
	}
	
	public virtual byte WriteMemory16(uint address, ushort data)
	{
		return 0;
	}
	*/
	// Our video functions.  There are two sides to video, the internal renderer
	// and the external display.
	// FIXME: I need a way to think about scanlines in an abstract way
	public virtual void RenderNextScanline()
	{
		return;
	}
	
	public virtual void DisplayToVideo()
	{
		return;
	}
	
	public virtual void InitializeEngine()
	{
		return;
	}
	
	// Our cart load function
	// FIXME: look up error handling in C# so failures can be handled correctly
	
	public virtual byte LoadCart(string filename, string numinstructions)
	{
		return 0;
	}
	
	public virtual void RunCart()
	{
		return;
	}
	
}