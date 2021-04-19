export class UrlHelper {
    static nameToUrlPart(name) {
        name = name.replaceAll(' ', '-');
        name = name.toLowerCase();

        return name;
    }

    static urlPartToName(urlPart) {
        urlPart = urlPart.replaceAll('-', ' ');

        return urlPart;
    }
}
