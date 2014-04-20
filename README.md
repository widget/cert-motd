cert-motd
=========

Looking to pull CERT coding standard into a machine parseable format, and then use that
for message-of-the-day type stuff.  I am assuming that the CERT standard cannot be distributed
so instead a script is provided to do this.

Dependencies
------------

For the database generator:

* Python3
* BeautifulSoup 4

Usage
-----

The db generator runs by default on the CERT C website, and produces a JSON list of all the rules and 
recommendations.  The provided `install.sh` script for Linux will put the database in `/usr/share/fortune/cert_motd`, or you can
run it manually, save the database where you like, and call the fortune script with the path to the
database as you like.

Displays
--------

###Command-line

There's a Python script that will display a tooltip in plain-text (or JSON, should do HTML at some point)

###.NET

Windows.Forms solution available, it takes a single argument to the path of the database, or it 
expects `db.json` to be in the same directory.

Future
-----

* I'd like to pull the first paragraph for each rule.  It will slow down database generation by an order of 
  magnitude without multiprocessing though.