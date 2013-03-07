from SubstringLocator import DBConn

def main():
	db = DBConn()
	x = db.containsSubstring('xyz')
	print x

if __name__ == '__main__':
	main()