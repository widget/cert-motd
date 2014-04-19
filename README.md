cert-motd
=========

Looking to pull CERT coding standard into a machine parseable format, and then use that
for message-of-the-day type stuff.

Dependencies
------------

For the scraper:

 * Python3
 * BeautifulSoup 4

Usage
-----

The scraper runs by default on the CERT C website, and produces a JSON object of all the rules and recommendations.