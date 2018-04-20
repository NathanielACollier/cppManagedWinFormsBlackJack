#include "stdafx.h"
#include "Form1.h"
#include <windows.h>
#include "Player.h"
#include <ctime>

using namespace blackjack;



int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
	srand(clock()); // seeds the pseudo random number generator with
	                // the clock function wich changes every second (produces best random numbers)
	System::Threading::Thread::CurrentThread->ApartmentState = System::Threading::ApartmentState::STA;
	Application::Run(new Form1());
	return 0;
}
