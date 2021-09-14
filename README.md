# ClipTabbedText
Simple application that stores text in a SQLite database that can be put into the clipboard.

The database is a SQLite database in the the user's Local App Data folder with a table that can be created using:

```
CREATE TABLE "ClipText" (
	"Description"	TEXT NOT NULL UNIQUE,
	"Data"	TEXT NOT NULL
)
```

When the application is used and an item is clipped the Data field is put into the clipboard.

The database must be created external to (not by) the program and the data edited external to the program.

The application is called ClipTabbedText because originally the data was in a tab-delimited file.
