import { AlertDialog, AlertDialogBody, AlertDialogContent, AlertDialogFooter, AlertDialogHeader, AlertDialogOverlay, Button, FormControl, FormLabel, Input, NumberDecrementStepper, NumberIncrementStepper, NumberInput, NumberInputField, NumberInputStepper, Select, SelectField, Text } from "@chakra-ui/react";
import { FocusableElement } from "@chakra-ui/utils";
import { RefObject, useRef } from "react";

interface Props {
    onClose: () => void
    isOpen: boolean
}

export const RoomCreationForm = ({ onClose, isOpen }: Props) => {

    const cancelRef = useRef<HTMLButtonElement | FocusableElement>(null);

    return (
        <AlertDialog
            motionPreset="slideInBottom"
            onClose={onClose}
            isOpen={isOpen}
            leastDestructiveRef={cancelRef}
            isCentered
        >

            <AlertDialogOverlay />

            <AlertDialogContent bg="white">
                <AlertDialogHeader fontSize={18} color="green">
                    <Text>
                        New room
                    </Text>
                </AlertDialogHeader>

                <AlertDialogBody>
                    <FormControl>

                        <div className="mb-5">
                            <FormLabel>Name</FormLabel>
                            <Input placeholder="name" />
                        </div>

                        <div className="mb-5">
                            <FormLabel>Password (if needed)</FormLabel>
                            <Input placeholder="password" type="password" />
                        </div>

                        <div className="mb-5">
                            <FormLabel>number of players</FormLabel>
                            <SelectField>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                                <option>5</option>
                                <option>6</option>
                            </SelectField>
                        </div>
                    </FormControl>

                </AlertDialogBody>


                <AlertDialogFooter>
                    <Button colorScheme="green">Create</Button>
                    <Button colorScheme="red" ml={3} onClick={onClose}>Cancel</Button>
                </AlertDialogFooter>

            </AlertDialogContent>
        </AlertDialog>

    );
}