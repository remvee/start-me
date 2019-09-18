MCS=mcs
MCS_FLAGS=

.PHONY: test clean

%.exe: %.cs
	$(MCS) $(MCS_FLAGS) $<

all: start_me_handler.exe

test: all
	mono start_me_handler.exe /say-ok

clean:
	rm -f *.exe
