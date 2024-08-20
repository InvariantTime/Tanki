import { Center, Flex, Image, Text } from "@chakra-ui/react";

export const Header = () => {
    return (
        <div className="p-1 flex items-center justify-between 
            bg-white border-b border-slate-300 shadow-lg">
            
            <HeaderLogo/>
            <nav>
                <ul className="flex gap-5 text-gray-400 ">
                    <li className="cursor-pointer">How to play</li>
                    <li className="cursor-pointer">Leaders</li>
                </ul>
            </nav>

            <div>
                Account Here
            </div>

        </div>
    );
}

const HeaderLogo = () =>
{
    return (
        <div className="items-center p-2 flex flex-row cursor-pointer">
            <Image src={process.env.PUBLIC_URL + "/img/logo.png"}
                w="60px" mr="10px"/>
            <Text as="b" fontSize={24}>
                Tanki
            </Text>
        </div>
    );
}