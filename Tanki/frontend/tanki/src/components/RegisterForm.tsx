import { AbsoluteCenter, Button, Heading, Input, Text } from "@chakra-ui/react";

export const RegisterForm = () => {
    return (
        <AbsoluteCenter>

            <form className="p-14 max-w-screen-lg 
                bg-white rounded border border-slate-300 shadow-xl">

                <Heading className="text-center mb-8">
                    Register
                </Heading>

                <div className="mb-7">
                    <Text>Name</Text>
                    <Input borderColor="GrayText" placeholder="input name"></Input>
                </div>

                <div className="mb-7">
                    <Text>Password</Text>
                    <Input type="password" borderColor="GrayText" placeholder="input password"></Input>
                </div>

                <div className="mb-7">
                    <Text>Confirm password</Text>
                    <Input type="password" borderColor="GrayText" placeholder="input password"></Input>
                </div>

                <div className="items-center w-full justify-center flex">
                    <Button type="submit" colorScheme="green">
                        Submit
                    </Button>
                </div>
            </form>
        </AbsoluteCenter>
    );
}