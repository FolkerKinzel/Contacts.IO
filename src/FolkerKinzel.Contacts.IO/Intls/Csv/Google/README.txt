GOOGLE:

Google seems to use only English column names and they seem to be constant. Only the first columns 
("Name" to "Group Membership") appear in every CSV-file (in constant order) while the others are
used if required.

Because of this, the mapping and CsvRecordWrapper may only contain the columns we want to read from 
or to write to - the index doesn't matter, because it's not constant.