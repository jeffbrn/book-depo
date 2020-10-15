import SiteBookData from './site-book-data';

export default class RawBookData {
  public isbn: string;

  public importedOn: Date;

  public bookFinder: SiteBookData | null;

  public isbnDb: SiteBookData | null;

  public openLibrary: SiteBookData | null;

  constructor() {
    this.isbn = '';
    this.importedOn = new Date();
    this.bookFinder = null;
    this.isbnDb = null;
    this.openLibrary = null;
  }
}
