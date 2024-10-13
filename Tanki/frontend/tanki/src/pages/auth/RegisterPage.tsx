import { Button, FormControl, FormLabel, Heading, Input, Text, Link, FormErrorMessage } from "@chakra-ui/react";
import { SyntheticEvent, useState } from "react";
import { redirect, useNavigate } from "react-router-dom";

const registerUrl = "http://localhost:5074/api/user/register";

export const RegisterPage = () => {

    const navigate = useNavigate();
    const [name, setName] = useState("");
    const [pass, setPass] = useState("");
    const [confirmPass, setConfirmPass] = useState("");
    const [loading, setLoading] = useState(false);

    const onSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();

        if (confirmPass != pass) {
            alert("confirm password is not equal password");
            return;
        }

        setLoading(true);

        const data = {
            name: name,
            password: pass
        };

        const options: RequestInit = {
            method: "POST",
            credentials: "include",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        };

        var result = await fetch(registerUrl, options);

        if (result.ok) {
            navigate("/");
        }
        else {
            const err = await result.text();
            setLoading(false);
            alert(err);
        }
    }

    return (
        <div className="flex w-full h-full justify-center items-center">
            <div className="bg-white p-10 border border-slate-300 
                shadow-xl rounded">
                <form onSubmit={onSubmit}>
                    <Heading mb="5" textAlign="center">
                        Register
                    </Heading>

                    <FormControl mb="4">
                        <FormLabel>Name</FormLabel>
                        <Input required borderColor="GrayText" placeholder="input name" onChange={x => setName(x.target.value)} />
                    </FormControl>

                    <FormControl mb="4">
                        <FormLabel>Password</FormLabel>
                        <Input required borderColor="GrayText" type="password" placeholder="input password" onChange={x => setPass(x.target.value)} />
                    </FormControl>

                    <FormControl mb="4">
                        <FormLabel>Confirm password</FormLabel>
                        <Input required borderColor="GrayText" type="password" placeholder="input password" onChange={x => setConfirmPass(x.target.value)} />
                    </FormControl>

                    <div className="text-center mb-4">
                        <Button type="submit" colorScheme="green" isLoading={loading} loadingText="Submitting">
                            Submit
                        </Button>
                    </div>
                </form>

                <Text textAlign="center">
                    already have an account? {' '}
                    <Link color="blue" href="/signin">
                        sign in
                    </Link>
                </Text>
            </div>
        </div>
    );
}