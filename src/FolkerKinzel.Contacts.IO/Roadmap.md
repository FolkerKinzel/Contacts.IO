# FolkerKinzel.Contacts.IO
## Roadmap

### 1.3.1
- [x] Passing an undefined enum value as VCardVersion will cause an ArgumentException.

### 2.0.0
- [ ] Remove obsolete symbols.
- [ ] The Load methods should return `ReadOnlyCollection<Contact>`
- [ ] The Write methods should only accept `IEnumerable<Contact>`
- [ ] Update FolkerKinzel.Contacts to 2.0.0
- [ ] Update FolkerKinzel.VCards to 4.0.0.

### 3.0.0
- [ ] End .NET Framework 4.0 support.
- [ ] Update FolkerKinzel.Contacts to 3.0.0
- [ ] Update FolkerKinzel.VCards to 5.0.0.
- [ ] Update FolkerKinzel.CsvTools to 2.0.0.
- [ ] Make the read and write methods ready to support classes, which are
derived from `Contact`

