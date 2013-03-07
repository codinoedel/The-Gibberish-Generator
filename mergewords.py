#-*- coding: utf_8 -*-

from os import listdir
from os.path import isfile, join
import sqlite3

class DataBase:
	# Creates file if file does not exist
	f = open('./dictionary_database.db', 'w')
	f.close()

	conn = sqlite3.connect('dictionary_database.db')
	c = conn.cursor()

	c.execute('''CREATE TABLE words
					(word text UNIQUE)''')


	def insertWord(self, s):
		word = (s.decode('latin-1'),)
		try:
			self.c.execute("INSERT INTO words (word) VALUES (?)", word)
		except sqlite3.IntegrityError:
			pass


	def commitChanges(self):
		self.conn.commit()

def CopyWordsToDatabase():
	directory = listdir("./Dictionary lists")
	db = DataBase()

	for f in directory:
		dictlist = open('./Dictionary lists/%s' % f, 'r')
		word = dictlist.readline().rstrip('\n')
		while len(word) != 0:
			db.insertWord(word)
			word = dictlist.readline().rstrip('\n')
		dictlist.close()
		db.commitChanges()	

	

def main():
	CopyWordsToDatabase()

if __name__ == "__main__":
	main()