#!/usr/bin/python3
#
# Scraper of CERT website to produce a JSON-format C coding standard for individual use

import argparse
from bs4 import BeautifulSoup
import json
import urllib.request
from urllib.error import URLError
from urllib.parse import urlparse

CERT_TOP_URL = "https://www.securecoding.cert.org/confluence/display/seccode/CERT+C+Coding+Standard"

def parse_from_title(tp, title_node):
	children = []
	for c in title_node.parent.ul.find_all('a'):
		child = dict()
		child["url"] = tp.scheme + "://" + tp.netloc + c.get('href')
		if '. ' in c.get_text():
			txt = c.get_text().split('. ')
			child["num"] = txt[0]
			child["name"] = ". ".join(txt[1:])
		else:
			child["num"]  = '??'
			child["name"] = c.get_text()
			
		children.append(child)
	return children


try:
	parser = argparse.ArgumentParser(description='Parse the CERT confluence page')
	parser.add_argument('-i', '--input', help='Input URL, top page of CERT C coding standard', default=CERT_TOP_URL)
	parser.add_argument('-o', '--output', help='JSON output', default='cert_db.json')

	args = parser.parse_args()

	print("Parsing from " + args.input)
	top = urllib.request.urlopen(args.input)
	top_parse = urlparse(args.input)
	topsoup = BeautifulSoup(top)
	index = topsoup.find(id='CERTCCodingStandard-SectionIndex')

	print("Found %d sections" % len(index.parent.ul.find_all('a')))

	sections = parse_from_title(top_parse, index)
	allchildren = []

	for s in sections:
		
		print("Found section '%s', following %s" % (s["name"], s["url"]))
		
		secpage = BeautifulSoup(urllib.request.urlopen(s["url"]))
		sectitles = secpage.find_all('h2')
		
		children = []
		for t in sectitles:
			try:
				c2 = []
				if '-Recommendations' in t.get('id'):
					c2 = parse_from_title(top_parse, t)
					for c in c2:
						c["type"] = "rule"
				elif '-Rules' in t.get('id'):
					c2 = parse_from_title(top_parse, t)
					for c in c2:
						c["type"] = "recommendation"
				
				if len(c2) > 0:
					children += c2
			except TypeError:
				pass
		
		for c in children:
			c["section"] = s["name"]
			c["sec-num"] = s["num"]
		allchildren += children
		
		print("Found %d rules/recommendations" % ( len(children)))
		
	print("Saving output")
	with open(args.output, 'w') as out: # and truncate
		json.dump(allchildren, out)
		
except URLError as e:
	print("Failed to access URL: %d - %s" % (e.code, e.reason))
except KeyboardInterrupt:
	print("Halting")
	