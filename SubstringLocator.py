import sqlite3

class DBConn:
	conn = sqlite3.connect('./dictionary_database.db')
	c = conn.cursor()

	def containsSubstring(self, s):
		substring = ('%'+s+'%',)
		self.c.execute("SELECT * FROM words WHERE word LIKE ?", substring)
		words = self.c.fetchall()
		wordcount = len(words)
		if wordcount > 0:
			self.insertMatch(s, int(wordcount))
			return True
		else:
			self.insertNonMatch(substring)
			return False

	def insertMatch(self, s, numMatches):
		s = (s,)
		try:
			self.c.execute("INSERT INTO consonant_substrings_in_english (substr, occurences) VALUES (?, ?)", (s, numMatches))
			self.conn.commit()
		except sqlite3.IntegrityError:
			pass

	def insertNonMatch(self, s):
		s = (s,)

		try:
			self.c.execute("INSERT INTO consonant_substrings_not_in_english (substr) VALUES (?)", s)
			self.conn.commit()
		except sqlite3.IntegrityError:
			pass



class GenerateSubstring:
	consonants = ['b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z']
	
	def substring(self, i):
		x = 1
		

	def tuples(self):
		substringlist = []

		for consonant1 in self.consonants:
			for consonant2 in self.consonants:
				if ()
				substringlist.append(consonant1 + consonant2)
		return substringlist

	def threeples(self):
		substringlist = []

		for consonant1 in self.consonants:
			for consonant2 in self.consonants:
				for consonant3 in self.consonants:
					substringlist.append(consonant1 + consonant2 + consonant3)
		return substringlist

	def fourples(self):
		substringlist = []

		for consonant1 in self.consonants:
			for consonant2 in self.consonants:
				for consonant3 in self.consonants:
					for consonant4 in self.consonants:
						substringlist.append(consonant1 + consonant2 + consonant3 + consonant4)
		return substringlist

def deleteDuplicates(comp, nomatches):
	print comp
	print nomatches

	for word in nomatches: # iterates over all the non-matches
		for plet in comp: # iterates over all the comparators to find comparators whose substring is in the non-matches list
			if word in plet:
				print "removed %s from the comparators list." % plet
				comp.remove(plet)
	return comp

def findNoMatches(comp, db):
	contains = False
	nomatches = []

	for comparator in comp:
		contains = db.containsSubstring(comparator)
		if (contains == False):
			nomatches.append(comparator)
			print "%s is not in the English language." % comparator
	return nomatches

def main():
	db = DBConn()
	subStr = GenerateSubstring()
	notuples = []
	nothreeples = []
	nofourples = []

	comp = subStr.tuples()

	# check for groups of two consonants that do not appear in english
	notuples = findNoMatches(comp, db)
	print notuples
	# remove threeples that contain the tuples listed with no english words associated with them
	comp = deleteDuplicates(subStr.threeples(), notuples)
	print comp
	# check for groups of three consonants that do not appear in english
	nothreeples = findNoMatches(comp, db)

	# remove the threeples and twoples from the fourples 
	comp = deleteDuplicates(subStr.fourples(), nothreeples)

	nofourples = findNoMatches(comp, db)

	# print "English does not contain these consonant strings: %s" % (notuples + nothreeples + nofourples)







if __name__ == '__main__':
	main()

