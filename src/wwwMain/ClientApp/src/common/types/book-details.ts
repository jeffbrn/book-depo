export default class BookDetails {
  public id: string;

  public isbn: string;

  public title: string;

  public author: string;

  public publishedOnRaw: string;

  public publishedOn: Date | null;

  public publisher: string;

  public numPages: number | null;

  public description: string;

  constructor() {
    this.id = '';
    this.isbn = '';
    this.title = '';
    this.author = '';
    this.publishedOnRaw = '';
    this.publishedOn = null;
    this.publisher = '';
    this.numPages = null;
    this.description = '';
  }
}
