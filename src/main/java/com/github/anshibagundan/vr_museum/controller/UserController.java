package com.github.anshibagundan.vr_museum.controller;

import com.github.anshibagundan.vr_museum.dto.UserDto;
import com.github.anshibagundan.vr_museum.service.UserService;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;


@AllArgsConstructor
@RestController
@RequestMapping("/api/user")
public class UserController {

    private UserService userService;

    @PostMapping
    public ResponseEntity<UserDto> createUser(@RequestBody UserDto userDto) {
        UserDto savedUser = userService.createUser(userDto);
        return new ResponseEntity<>(savedUser, HttpStatus.CREATED);
    }

    @GetMapping("{id}")
    public ResponseEntity<UserDto> getUserByID(@PathVariable Long userId) {
        UserDto userDto = userService.getUserById(userId);
        return ResponseEntity.ok(userDto);
    }

    @GetMapping
    public ResponseEntity<List<UserDto>> getAllUsers() {
        List<UserDto> users = userService.getAllUser();
        return ResponseEntity.ok(users);
    }

    @PutMapping("{id}")
    public ResponseEntity<UserDto> updateUser(@PathVariable("id") Long UsersId, @RequestBody UserDto updatedUser) {
        UserDto userDto = userService.updateUser(UsersId, updatedUser);
        return ResponseEntity.ok(userDto);
    }

    @DeleteMapping({"id"})
    public ResponseEntity<String> deleteUser(@PathVariable("id") Long UsersId) {
        userService.deleteUser(UsersId);
        return ResponseEntity.ok("User deleted successfully");
    }
}
