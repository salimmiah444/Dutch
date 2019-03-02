class StoreCustomer {

    constructor(private firsName: string, private lastName: string) {

    }

    public visits: number = 0;
    private ourName: string;

    public showName() {
        alert(this.firsName + " " + this.lastName);

    }

    set name(val) {
        this.ourName = val;
    }

    get name() {
        return this.ourName;
    }

}
