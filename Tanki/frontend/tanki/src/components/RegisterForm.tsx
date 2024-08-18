import { AbsoluteCenter, Button, Center, Heading, Input, Text } from "@chakra-ui/react";

export const RegisterForm = () => {
    return (
        <AbsoluteCenter>
            <form className="p-20 max-w-sm w-full bg-gray-800 rounded border border-gray-700 shadow-lg shadow-blue-950">

                <Heading className="text-center mb-8 text-slate-200">
                    Register
                </Heading>

                <div className="mb-7">
                    <Text className="text-slate-400">Name</Text>
                    <Input borderColor="GrayText" placeholder="input name"></Input>
                </div>

                <div className="mb-7">
                    <Text className="text-slate-400">Password</Text>
                    <Input type="password" borderColor="GrayText" placeholder="input password"></Input>
                </div>

                <div className="items-center w-full justify-center flex">
                    <Button type="submit" colorScheme="blue">
                        <div className="text-slate-200">
                            Submit
                        </div>
                    </Button>
                </div>
            </form>
        </AbsoluteCenter>
    );
}