import { AlertDialog, AlertDialogBody, AlertDialogContent, AlertDialogFooter, AlertDialogHeader, AlertDialogOverlay, Button, FormControl, FormLabel, Input, NumberDecrementStepper, NumberIncrementStepper, NumberInput, NumberInputField, NumberInputStepper, Select, SelectField, Text } from "@chakra-ui/react";
import { FocusableElement } from "@chakra-ui/utils";
import { FormEventHandler, MouseEventHandler, RefObject, SyntheticEvent, useRef, useState } from "react";

interface Props {
    onClose: () => void
    isOpen: boolean
}

export const RoomCreationForm = ({ onClose, isOpen }: Props) => {

    const cancelRef = useRef<HTMLButtonElement | FocusableElement>(null);

    function onSubmit(e: SyntheticEvent) {
        e.preventDefault();
    }

    return (
        <AlertDialog
            motionPreset="slideInBottom"
            onClose={onClose}
            isOpen={isOpen}
            leastDestructiveRef={cancelRef}
            isCentered>

            <AlertDialogOverlay />

            <AlertDialogContent bg="white">
                <AlertDialogHeader fontSize={18} color="green">
                    <Text>
                        New room
                    </Text>
                </AlertDialogHeader>

                <form onSubmit={onSubmit}>
                    <AlertDialogBody>

                        <FormControl mb="4">
                            <FormLabel>Name</FormLabel>
                            <Input placeholder="name" required />
                        </FormControl>

                        <FormControl mb="4">
                            <FormLabel>Password (if needed)</FormLabel>
                            <Input placeholder="password" type="password" />
                        </FormControl>

                        <FormControl mb="4">
                            <FormLabel>number of players</FormLabel>
                            <SelectField>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                                <option>5</option>
                                <option>6</option>
                            </SelectField>
                        </FormControl>
                    </AlertDialogBody>


                    <AlertDialogFooter>
                        <Button colorScheme="green" type="submit">Create</Button>
                        <Button colorScheme="red" ml={3} onClick={onClose}>Cancel</Button>
                    </AlertDialogFooter>
                </form>

            </AlertDialogContent>
        </AlertDialog >

    );
}