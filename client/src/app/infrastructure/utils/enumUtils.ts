export const getEnumValues = (enumObj: any): [string, ...string[]] => {
    const values = Object.values(enumObj).filter(value => typeof value === 'string');

    if (values.length === 0) {
        throw new Error("Enum must have at least one string value");
    }

    return values as [string, ...string[]]
};