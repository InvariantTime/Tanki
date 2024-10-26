import { Button, extendTheme, Text } from "@chakra-ui/react"
import { Dispatch, SetStateAction, useState } from "react";
import { FaLongArrowAltLeft, FaLongArrowAltRight, FaEllipsisH } from "react-icons/fa"

const maxControlPages = 5;
const buttonCount = 7;

interface Props {
    totalCount: number,
    page: number,
    setPage : Dispatch<SetStateAction<number>>
}

export const PaginationControl = ({ totalCount, page, setPage }: Props) => {

    if (totalCount <= 0)
        return (<></>);

    const PaginationButton = (index: number) => {
        const isDisabled = index == page;

        if (isDisabled === true)
            return (
                <Button bg="green.500" color="white"
                    _hover={{ bg: "green.500" }}>
                    {index}
                </Button>
            );

        return (
            <Button bg="green.100"
                _hover={{ bg: "green.200" }}
                onClick={x => setPage(index)}>
                {index}
            </Button>
        );
    }

    const PaginatitionEllipsis = () =>
    {
        return (<div className="text-center items-center flex p-2">
            <FaEllipsisH/>
        </div>)
    }

    const PaginationElement = (index: number) : JSX.Element =>
    {
        const first = page < maxControlPages;
        const last = totalCount - page + 1 < maxControlPages;

        if (index === 1)
            return PaginationButton(1);

        if (index === buttonCount)
            return PaginationButton(totalCount);

        if (first && index <= maxControlPages) {
            return PaginationButton(index);
        }
        else if (last && index > 2) {
            return PaginationButton(totalCount - (buttonCount - index));
        }
        else {
            if (index === 3) {
                return PaginationButton(page - 1);
            }
            else if (index === 4) {
                return PaginationButton(page);
            }
            else if (index === 5) {
                return PaginationButton(page + 1);
            }
            else {
                return PaginatitionEllipsis();
            }
        }
    }

    const PaginationBody = () => {
        const elements: JSX.Element[] = [];

        for (var i = 1; i <= buttonCount && i <= totalCount; i++)
            elements.push(PaginationElement(i));

        return (elements)
    }

    const PaginationArrow = ({disabled, children, onClick}: {disabled: boolean, children: JSX.Element, onClick:Function}) => {
        return (
            <Button bg={disabled ? "green.100" : "green.500"}
                    _hover={{ bg: disabled ? "green.100" : "green.400" }}
                    color={disabled ? "black" : "white"}
                    cursor={disabled ? "default" : "pointer" }
                    onClick={x => {
                        if (disabled === false) {
                            onClick()
                        }
                    }}>
                    {children}
                </Button>
        )
    }

    return (
        <div className="p-2 flex w-max">
            <div className="gap-2 flex">
                <PaginationArrow disabled={page === 1} onClick={() => setPage(page - 1)}>
                    <FaLongArrowAltLeft />
                </PaginationArrow>
                
                {PaginationBody()}

                <PaginationArrow disabled={page === totalCount} onClick={() => setPage(page + 1)}>
                    <FaLongArrowAltRight />
                </PaginationArrow>
            </div>
        </div>
    )
}