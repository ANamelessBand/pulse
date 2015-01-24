################################################################################
# Automatically-generated file. Do not edit!
################################################################################

SHELL := cmd.exe
RM := rm -rf

USER_OBJS :=

LIBS := 
PROJ := 

O_SRCS := 
C_SRCS := 
S_SRCS := 
S_UPPER_SRCS := 
OBJ_SRCS := 
ASM_SRCS := 
PREPROCESSING_SRCS := 
OBJS := 
OBJS_AS_ARGS := 
C_DEPS := 
C_DEPS_AS_ARGS := 
EXECUTABLES := 
OUTPUT_FILE_PATH :=
OUTPUT_FILE_PATH_AS_ARGS :=
AVR_APP_PATH :=$$$AVR_APP_PATH$$$
QUOTE := "
ADDITIONAL_DEPENDENCIES:=
OUTPUT_FILE_DEP:=

# Every subdirectory with source files must be described here
SUBDIRS := 


# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS +=  \
../main.c \
../../usbdrv/oddebug.c \
../../usbdrv/usbdrv.c


PREPROCESSING_SRCS +=  \
../../usbdrv/usbdrvasm.S


ASM_SRCS += 


OBJS +=  \
main.o \
usbdrvasm.o \
oddebug.o \
usbdrv.o


OBJS_AS_ARGS +=  \
main.o \
usbdrvasm.o \
oddebug.o \
usbdrv.o


C_DEPS +=  \
main.d \
oddebug.d \
usbdrv.d


C_DEPS_AS_ARGS +=  \
main.d \
oddebug.d \
usbdrv.d


OUTPUT_FILE_PATH +=cdcio.elf

OUTPUT_FILE_PATH_AS_ARGS +=cdcio.elf

ADDITIONAL_DEPENDENCIES:=

OUTPUT_FILE_DEP:= ./makedep.mk

# AVR32/GNU C Compiler







./%.o: .././%.c
	@echo Building file: $<
	@echo Invoking: AVR/GNU C Compiler : (AVR_8_bit_GNU_Toolchain_3.4.1_830) 4.6.2
	$(QUOTE)F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-gcc.exe$(QUOTE) -DF_CPU=12000000UL  -I"..\." -I"..\..\usbdrv"  -Os -Wall -c -gdwarf-2 -fsigned-char  -MD -MP -MF "$(@:%.o=%.d)" -MT"$(@:%.o=%.d)" -MT"$(@:%.o=%.o)"  -mmcu=atmega8   -o"$@" "$<" 
	@echo Finished building: $<

./%.o: ../../usbdrv/%.c
	@echo Building file: $<
	@echo Invoking: AVR/GNU C Compiler : (AVR_8_bit_GNU_Toolchain_3.4.1_830) 4.6.2
	$(QUOTE)F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-gcc.exe$(QUOTE) -DF_CPU=12000000UL  -I"..\." -I"..\..\usbdrv"  -Os -Wall -c -gdwarf-2 -fsigned-char  -MD -MP -MF "$(@:%.o=%.d)" -MT"$(@:%.o=%.d)" -MT"$(@:%.o=%.o)"  -mmcu=atmega8   -o"$@" "$<" 
	@echo Finished building: $<



# AVR32/GNU Preprocessing Assembler



# AVR32/GNU Assembler
./usbdrvasm.o: ../../usbdrv/usbdrvasm.s
	@echo Building file: $<
	@echo Invoking: AVR32/GNU Assembler : (AVR_8_bit_GNU_Toolchain_3.4.1_830) 4.6.2
	$(QUOTE)F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-gcc.exe$(QUOTE) -Wa,-gdwarf2 -x assembler-with-cpp -c -Wall -gdwarf-2 -DF_CPU=12000000UL -Os -fsigned-char -I "..\." -I "..\..\usbdrv"   -mmcu=atmega8   -o"$@" "$<"
	@echo Finished building: $<


./%.o: ../../usbdrv/%.s
	@echo Building file: $<
	@echo Invoking: AVR32/GNU Assembler : (AVR_8_bit_GNU_Toolchain_3.4.1_830) 4.6.2
	$(QUOTE)F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-gcc.exe$(QUOTE) -Wa,-gdwarf2 -x assembler-with-cpp -c -Wall -gdwarf-2 -DF_CPU=12000000UL -Os -fsigned-char -I "..\." -I "..\..\usbdrv"   -mmcu=atmega8   -o"$@" "$<"
	@echo Finished building: $<




ifneq ($(MAKECMDGOALS),clean)
ifneq ($(strip $(C_DEPS)),)
-include $(C_DEPS)
endif
endif

# Add inputs and outputs from these tool invocations to the build variables 

# All Target
all: $(OUTPUT_FILE_PATH) $(ADDITIONAL_DEPENDENCIES)

$(OUTPUT_FILE_PATH): $(OBJS) $(USER_OBJS) $(OUTPUT_FILE_DEP)
	@echo Building target: $@
	@echo Invoking: AVR/GNU Linker : (AVR_8_bit_GNU_Toolchain_3.4.1_830) 4.6.2
	$(QUOTE)F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-gcc.exe$(QUOTE) -o$(OUTPUT_FILE_PATH_AS_ARGS) $(OBJS_AS_ARGS) $(USER_OBJS) $(LIBS) -Wl,-Map="cdcio.map" -Wl,--start-group  -Wl,--end-group  -mmcu=atmega8  
	@echo Finished building target: $@
	"F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-objcopy.exe" -O ihex -R .eeprom -R .fuse -R .lock -R .signature  "cdcio.elf" "cdcio.hex"
	"F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-objcopy.exe" -j .eeprom  --set-section-flags=.eeprom=alloc,load --change-section-lma .eeprom=0  --no-change-warnings -O ihex "cdcio.elf" "cdcio.eep" || exit 0
	"F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-objdump.exe" -h -S "cdcio.elf" > "cdcio.lss"
	"F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-objcopy.exe" -O srec -R .eeprom -R .fuse -R .lock -R .signature  "cdcio.elf" "cdcio.srec"
	"F:\AVR\Atmel Studio 6.0\extensions\Atmel\AVRGCC\3.4.1.95\AVRToolchain\bin\avr-size.exe" "cdcio.elf"
	





# Other Targets
clean:
	-$(RM) $(OBJS_AS_ARGS)$(C_DEPS_AS_ARGS) $(EXECUTABLES) 
	rm -rf "cdcio.elf" "cdcio.a" "cdcio.hex" "cdcio.lss" "cdcio.eep" "cdcio.map" "cdcio.srec"
	