
set /p I_ADDR=<%userprofile%\Desktop\NGUIndustriesInjector\addr.data

IF DEFINED I_ADDR (
	echo Unloading old version
	rem %0\..\injector\smi.exe eject -p "NGU INDUSTRIES" -a "%I_ADDR:~27%" -n NGUIndustriesInjector -c Loader -m Unload
	rem timeout /t 10
)
ELSE (echo Not unloading)

%0\..\injector\smi.exe inject -p "NGU INDUSTRIES" -a %0\..\injector\NGUIndustriesInjector.dll -n NGUIndustriesInjector -c Loader -m Init > %userprofile%\Desktop\NGUIndustriesInjector\addr.data

