#!/usr/bin/python3

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
	parser.add_argument('-o', '--output', help='JSON output', default='standard.txt')

	args = parser.parse_args()

	print("Parsing from " + args.input)
	top = urllib.request.urlopen(args.input)
	top_parse = urlparse(args.input)
	topsoup = BeautifulSoup(top)
	index = topsoup.find(id='CERTCCodingStandard-SectionIndex')

	print("Found %d sections" % len(index.parent.ul.find_all('a')))

	sections = parse_from_title(top_parse, index)

	for s in sections:
		
		print("Found section '%s', following %s" % (s["name"], s["url"]))
		
		secpage = BeautifulSoup(urllib.request.urlopen(s["url"]))
		sectitles = secpage.find_all('h2')
		
		rules = []
		rec = []
		for t in sectitles:
			try:
				if '-Recommendations' in t.get('id'):
					rec = parse_from_title(top_parse, t)
				elif '-Rules' in t.get('id'):
					rules = parse_from_title(top_parse, t)
			except TypeError:
				pass
		
		print("Found %d rules, %d recommendations" % ( len(rec), len(rules)))
		for r in rules:
			print(r["name"])
		for r in rec:
			print(r["name"])
		s["rules"] = rules
		s["recs"] = rec 
		
	print("Saving output")
	with open(args.output, 'w') as out:
		json.dump(sections, out)
		
except URLError as e:
	print("Failed to access URL: %d - %s" % (e.code, e.reason))
except KeyboardInterrupt:
	print("Halting")
	