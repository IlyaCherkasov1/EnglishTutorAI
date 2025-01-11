export const getNameOfFunction = <T>(): (name: Extract<keyof T, string>) => string => {
    return (name: string) => name;
}