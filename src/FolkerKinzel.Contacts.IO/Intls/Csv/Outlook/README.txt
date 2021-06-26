Outlook:

Outlook itself seems to use the same (English) column names in a defined order. However, Google uses
a different order than the web examples, if it exports CSV in "Outlook-Format".

Reading-Strategy:

1.) Try the English column names.
2.) Believe in the known order of columns and access them
    by index. (The mapping targets the existing column names in their existing order.)

    (Accessing the columns by column name is better than by index, because it works regardless of the
    column order.)

    The mapping and CsvRecordWrapper must contain all known columns in the known standard order, because the file 
    is probably accessed by index!

Outlook seems to use culture dependent formatting for DateTime.