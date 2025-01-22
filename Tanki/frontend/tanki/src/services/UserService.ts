
const verifyUrl = "http://localhost:5074/api/account/verify";

export const invalid: UserData = {isSuccess: false, name: "", score: 0};

export class UserService
{
    public static async verify(): Promise<UserData>
    {
        const options: RequestInit =
        {
            method:"GET",
            credentials: "include"
        };

        try
        {
        var result = await fetch(verifyUrl, options);
        }
        catch
        {
            return invalid;
        }

        if (result.ok)
        {
            const data = await result.json();

            return {
                isSuccess: true,
                name: data.name,
                score: data.score
            };
        }

        return invalid;
    }
}

export type UserData =
{
    isSuccess: boolean,
    name: string,
    score: number
}