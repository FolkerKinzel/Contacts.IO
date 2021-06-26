Thunderbird:

CSV-Columns can have different names for each language, but count and order of columns are always equal.

Reading-Strategy:

1.) Try the English column names.
2.) Try the German column names.
3.) If neither the English nor the German column names match believe in the known order of columns and access them
    by index. (The mapping targets the existing column names in their existing order.)

    (Accessing the columns by column name is better than by index, because it works even if Thunderbird might change
    the column order.)

    The mapping and CsvRecordWrapper must contain all known columns in the known standard order, because the file 
    is probably accessed by index!