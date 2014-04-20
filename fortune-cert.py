#!/usr/bin/python3
#
# Prints out random CERT C Coding Standard rules based on a database that can be refreshed

import argparse
import json
import random
import sys

DBFILE = "/usr/share/fortune/cert_motd/db.json"

parser = argparse.ArgumentParser(description='Generate random CERT rule')
parser.add_argument('-i', '--input', help='Input CERT C coding standard database', default=DBFILE)
parser.add_argument('-f', '--format', help='Ouput format', default='txt')

args = parser.parse_args()

try:
	db = json.load(open(args.input))
except IOError as e:
	print("Can't find CERT database at " % args.input)
	sys.exit(1)

select = random.choice(db["rules"])

if args.format == "json":
	print(json.dumps(select, indent=4, separators=(',', ': ')))
elif args.format == "txt":
	print("""CERT {type} {num}: 
		
		{section} - {title}
		{url}
		""".format(type=select["type"], num=select["num"], section=select["section"],
					title=select["name"], url=select["url"]))
else:
	print("Unknown format")
	sys.exit(1)

