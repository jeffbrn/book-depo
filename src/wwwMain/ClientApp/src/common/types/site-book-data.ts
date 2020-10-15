export default class SiteBookData {
  public title: string;

  public author: string;

  public publisher: string;

  public datePublished: string;

  public numPages: number | null;

  public description: string;

  public coverImageUrl: string;

  public rawHtml: string;

  constructor() {
    this.title = '';
    this.author = '';
    this.publisher = '';
    this.datePublished = '';
    this.numPages = null;
    this.description = '';
    this.coverImageUrl = '';
    this.rawHtml = '';
  }
}
